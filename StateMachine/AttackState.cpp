#include "AttackState.h"
#include "DodgeState.h"
#include "JumpState.h"
#include "StateManager.h"
#include <chrono>
#include <iostream>
#include <memory>

AttackState::AttackState(StateManager& stateManager) : State(stateManager) {}

void AttackState::enter() {
    lastShot = std::chrono::steady_clock::now();
    std::cout << "[ATTACK] Entering attack state. Ammo: " << manager.ammoRemaining() << '\n';
}

void AttackState::update() {
    if (manager.isBulletThreatening()) {
        std::cout << "[ATTACK] Incoming bullet detected!\n";
        manager.changeState(std::make_unique<DodgeState>(manager));
        return;
    }

    constexpr auto shotInterval = std::chrono::milliseconds(500);
    const auto now = std::chrono::steady_clock::now();
    if (now - lastShot < shotInterval) return;
    lastShot = now;

    if (manager.ammoRemaining() > 0) {
        const int remaining = manager.consumeAmmo();
        std::cout << "[ATTACK] Shoot! Ammo remaining: " << remaining << '\n';
    }

    if (manager.ammoRemaining() == 0) {
        std::cout << "[ATTACK] Out of ammo. Moving to jump state.\n";
        manager.changeState(std::make_unique<JumpState>(manager));
    }
}
