#include "State.h"
#include "JumpState.h"

#include "StateManager.h"
#include "AttackState.h"

#include <Windows.h>

#include <iostream>

using namespace std;

int main(int argc, char* argv[])
{
	StateManager manager;

	cout << "Setting initial state to Attack..." << endl;
	Sleep(1000);
	manager.setState(new AttackState(&manager));

	while(true)
	{
		manager.update();
	}

	return 0;
}