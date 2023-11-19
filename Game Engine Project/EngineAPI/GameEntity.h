#pragma once
#include "..\Components\ComponentsCommon.h"
#include "TransformComponent.h"

namespace gerlie::game_entity
{
	DEFINE_TYPED_ID(entity_id);

	class entity
	{
	public:
		constexpr explicit entity(entity_id id) : _id{ id } {}
		constexpr entity() : _id{ id::invalid_id } {}
		constexpr entity_id get_id() const { return _id; }	//Method for returning ID.
		constexpr bool is_valid() const { return id::is_valid(_id); }	//Checking the validity of ID.

		transform::component transform() const;

	private:
		entity_id _id;
	};
}