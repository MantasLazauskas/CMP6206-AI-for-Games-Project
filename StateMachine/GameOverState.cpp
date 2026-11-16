#include "GameOverState.h"
#include "StateManager.h"
#include <iostream>

void GameOverState::enter() {
    std::cout << "[GAME OVER] The mini-game has ended.\n";
    manager.finish();
}

void GameOverState::update() {}
