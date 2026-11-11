#pragma once
#include "State.h"
#include <chrono>

class AttackState final : public State {
public:
    explicit AttackState(StateManager& manager);
    void enter() override;
    void update() override;
private:
    std::chrono::steady_clock::time_point lastShot{};
};

#endif