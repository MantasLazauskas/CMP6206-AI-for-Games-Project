#pragma once
#include "State.h"
#include <chrono>

class DodgeState final : public State {
public:
    using State::State;
    void enter() override;
    void update() override;
private:
    std::chrono::steady_clock::time_point started{};
};
