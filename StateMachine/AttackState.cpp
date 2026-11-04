#include "AttackState.h"
#include "StateManager.h"
#include "JumpState.h"

#include <Windows.h>

#include <iostream>

using namespace std;

AttackState::AttackState(StateManager* manager) : State(manager)
{
	ammo = 100;
}

void AttackState::update()
{
	if(ammo <= 0)
	{
		cout << "Out of ammo, changing state to jump..." << endl;
		Sleep(1000);
		manager->setState(new JumpState(manager));
	}
	else
	{
		cout << "Shoot" << endl;
		Sleep(250);
		ammo--;
		if (ammo % 10 == 0) {
			cout << "Ammo Count: " << ammo << endl;
		}
	}
}