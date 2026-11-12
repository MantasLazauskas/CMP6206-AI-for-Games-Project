#include "DodgeState.h"
#include "AttackState.h"
#include "StateManager.h"
#include <chrono>
#include <iostream>
#include <memory>

void DodgeState::enter() {
    started = std::chrono::steady_clock::now();
    std::cout << "[DODGE] Evading incoming bullet!\n";
}

void DodgeState::update() {
    constexpr auto dodgeDuration = std::chrono::milliseconds(900);
    if (std::chrono::steady_clock::now() - started >= dodgeDuration) {
        manager.setBulletThreat(false);
        std::cout << "[DODGE] Threat avoided. Returning to attack.\n";
        manager.changeState(std::make_unique<AttackState>(manager));
    }
}
