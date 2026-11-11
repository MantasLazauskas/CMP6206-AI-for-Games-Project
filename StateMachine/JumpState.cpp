#include "JumpState.h"
#include "GameOverState.h"
#include "StateManager.h"
#include <chrono>
#include <iostream>
#include <memory>

void JumpState::enter() {
    started = std::chrono::steady_clock::now();
    std::cout << "[JUMP] Character jumps!\n";
}

void JumpState::update() {
    constexpr auto jumpDuration = std::chrono::seconds(1);
    if (std::chrono::steady_clock::now() - started >= jumpDuration) {
        std::cout << "[JUMP] Jump completed.\n";
        manager.changeState(std::make_unique<GameOverState>(manager));
    }
}