#pragma once
#include "ComponentsCommon.h"

namespace gerlie::transform
{
	struct init_info//Initialization-information structure.
	{
		f32 position[3]{};	//Position.
		f32 rotation[4]{};	//Rotation quaternion.
		f32 scale[3]{ 1.f, 1.f, 1.f };	//3 components for scale with default value of 1 for each.
	};

	component create_transform(const init_info& info, game_entity::entity entity);	//Initialization-information structure that defines what entity ID a transform belongs to.
	void remove_transform(component c);	//Removes transforms.
}