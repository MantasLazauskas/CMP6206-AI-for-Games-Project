#pragma once

#include <memory>

class StateManager;

class State {
public:
    explicit State(StateManager& manager) noexcept;
    virtual ~State() = default; // Required for safe deletion through a base pointer.

    virtual void enter() {}
    virtual void update() = 0;
    virtual void exit() {}

protected:
    StateManager& manager;
};
