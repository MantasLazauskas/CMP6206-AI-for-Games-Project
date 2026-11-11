#include "IdleState.h"
#include "AttackState.h"
#include "StateManager.h"
#include <chrono>
#include <iostream>
#include <memory>

void IdleState::enter() {
    started = std::chrono::steady_clock::now();
    std::cout << "[IDLE] Character is waiting...\n";
}

void IdleState::update() {
    constexpr auto idleDuration = std::chrono::seconds(2);
    if (std::chrono::steady_clock::now() - started >= idleDuration) {
        std::cout << "[IDLE] Timer expired. Starting attack.\n";
        manager.changeState(std::make_unique<AttackState>(manager));
    }
}
