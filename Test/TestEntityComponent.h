#pragma once

#include "..\Test\Test.h"
#include "..\Game Engine Project\Components\Entity.h"
#include "..\Game Engine Project\Components\Transform.h"
#include <iostream>
#include <ctime>

using namespace gerlie;

class engine_test : public test
{
public:
	bool initialize() override
	{
		srand((u32)time(nullptr));
		return true;
	}
	void run() override
	{

	}
	void shutdown() override
	{
		do {
			for (u32 i{ 0 }; i < 10000; i++)
			{
				create_random();
				remove_random();
				//num_entities(u32)entities.size();
			}
		} while (getchar() != 'q');
	}

private:
	void create_random()
	{
		u32 count = rand() % 20;

		if (!entities.empty())
			count = 1000;
		
		transform::init_info transform_info{};
		game_entity::entity_info entity_info
		{
		};

	}

	void remove_random()
	{

	}

	utl::vector<game_entity::entity> entities;

	u32 added{ 0 };
	u32 removed{ 0 };
	u32 num_entiites{ 0 };
};
