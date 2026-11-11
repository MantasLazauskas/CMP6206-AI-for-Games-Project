#include "StateManager.h"
#include "IdleState.h"

#include <chrono>
#include <iostream>
#include <memory>
#include <thread>

int main() {
    StateManager manager;
    manager.changeState(std::make_unique<IdleState>(manager));

    std::cout << "FSM mini-game starting...\n";
    int frame = 0;
    while (!manager.isFinished()) {
        // Demo input: simulate incoming bullets at selected moments.
        // In a real game, a collision/perception system would set this flag.
        manager.setBulletThreat(frame == 28 || frame == 55);
        manager.update();
        ++frame;
        std::this_thread::sleep_for(std::chrono::milliseconds(100));
    }

    std::cout << "FSM demo finished.\n";
    return 0;
}
