#include "StateManager.h"
#include "State.h"

#include <utility>

void StateManager::changeState(std::unique_ptr<State> nextState) {
    // Defer the transition until the current state's update() has returned.
    pendingState = std::move(nextState);
}

void StateManager::applyPendingState() {
    if (!pendingState) return;

    if (currentState) currentState->exit();
    currentState = std::move(pendingState);
    currentState->enter();
}

void StateManager::update() {
    if (finished) return;

    // Apply a transition requested before this update, then run exactly one state.
    applyPendingState();
    if (currentState) currentState->update();
    // A state requested during update becomes active only after update() returns.
    applyPendingState();
}
