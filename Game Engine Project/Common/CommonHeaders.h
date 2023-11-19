#pragma once
#pragma warning(disable:4530)	//Disables exception warnings.

//C/C++

#include <stdint.h>
#include <assert.h>
#include <typeinfo>

#if defined(_WIN64)
#include <DirectXMath.h>
#endif
	
//CommonHeaders

#include "PrimitiveTypes.h"
#include "..\Utilities\Utilities.h"
#include "..\Utilities\MathTypes.h"