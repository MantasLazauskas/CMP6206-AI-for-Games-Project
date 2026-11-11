#pragma once

#include <memory>

class State;

class StateManager {
public:
    StateManager() = default;
    ~StateManager() = default;

    StateManager(const StateManager&) = delete;
    StateManager& operator=(const StateManager&) = delete;

    void changeState(std::unique_ptr<State> nextState);
    void update();

    void setBulletThreat(bool value) noexcept { bulletThreat = value; }
    bool isBulletThreatening() const noexcept { return bulletThreat; }
    int ammoRemaining() const noexcept { return ammo; }
    int consumeAmmo() noexcept { if (ammo > 0) --ammo; return ammo; }
    void finish() noexcept { finished = true; }
    bool isFinished() const noexcept { return finished; }

private:
    void applyPendingState();

    std::unique_ptr<State> currentState;
    std::unique_ptr<State> pendingState;
    int ammo = 10;
    bool bulletThreat = false;
    bool finished = false;
};
