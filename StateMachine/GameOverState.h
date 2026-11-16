#pragma once
#include "State.h"

class GameOverState final : public State {
public:
    using State::State;
    void enter() override;
    void update() override;
};
