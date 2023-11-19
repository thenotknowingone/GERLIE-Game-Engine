#pragma once
#include "CommonHeaders.h"

namespace gerlie::id
{
	using id_type = u32;

	namespace internal
	{
			//Max bits for the entity iteration counter and its index bits. 

		constexpr u32 generation_bits	//Entity iteration counter.
		{
			8
		};

		constexpr u32 index_bits	//id_type length minus the used bits for entity iteration counter.
		{
			sizeof(id_type) * 8 - generation_bits
		};
		constexpr id_type index_mask	//Masks indices.
		{
			(id_type{1} << index_bits) - 1
		};

		constexpr id_type generation_mask	//Masks generation bits.
		{
			(id_type{1} << generation_bits) - 1
		};
	}

	constexpr id_type invalid_id	//Masks IDs.
	{
		id_type(-1)
	};	
	
	constexpr u32 min_deleted_elements
	{
		1024
	};
	 
	using generation_type = std::conditional_t<internal::generation_bits <= 16, std::conditional_t<internal::generation_bits <= 8, u8, u16>, u32>;	//Dynamically adjusts generation bit size to save memory.
		
	static_assert(sizeof(generation_type) * 8 >= internal::generation_bits); 
	static_assert((sizeof(id_type) - sizeof(generation_type)) > 0);

	constexpr bool is_valid(id_type id)	//Checks if an ID is valid and is not equal to -1 (255).
	{
		return id != invalid_id;
	}

	constexpr id_type index(id_type id)	//Masks the ID and returns only the index part.
	{
		id_type index
		{
			id & internal::index_mask
		};
		assert(index != internal::index_mask);
		return index;
	}

	constexpr id_type generation(id_type id)	//Converts ID to generation bits by shifting bits to the left, then masks it.
	{
		return (id >> internal::index_bits) & internal::generation_mask;
	}

	constexpr id_type new_generation(id_type id)	//Increments inline id_type generation.
	{
		const id_type generation
		{
			id::generation(id) + 1
		};

		assert(generation < (((u64)1 << internal::generation_bits) - 1));
		return index(id) | (generation << internal::index_bits);
	}

#if _DEBUG

	namespace internal
	{
		struct id_base	//Simply contains u32 IDs and returns it upon request.
		{
			constexpr explicit id_base(id_type id) : _id{ id } {}	//Container algorithm.
			constexpr operator id_type() const { return _id; }	//Return operator.

		private:
			id_type _id;	//ID base private container.
		};
	}

#define DEFINE_TYPED_ID(name)							\
		struct name final : id::internal::id_base		\
		{												\
			constexpr explicit name(id::id_type id)		\
				: id_base{id} {}						\
			constexpr name() : id_base{0}{}				\
		}

#else

#define DEFINE_TYPED_ID(name) using name = id::id_type;

#endif
}