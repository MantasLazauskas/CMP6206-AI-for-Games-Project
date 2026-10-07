#include "JumpState.h"

#include <Windows.h>

#include <iostream>

using namespace std;

JumpState::JumpState(StateManager* manager) : State(manager)
{

}

void JumpState::update()
{
	cout << "Jump update" << endl;
	Sleep(250);
}