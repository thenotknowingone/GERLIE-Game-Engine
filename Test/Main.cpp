#pragma comment(lib, "Game Engine Project.lib")

#define TEST_ENTITY_COMPONENTS 1

#if TEST_ENTITY_COMPONENTS
#include "TestEntityComponent.h"
#else
#error One of the tests needs to be enabled.
#endif

int main()
{
#if _DEBUG
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);	//ON: Enable debug heap allocations and use of memory block type identifiers, such as _CLIENT_BLOCK. OFF: Add new allocations to heap's linked list, but set block type to _IGNORE_BLOCK. | ON: Perform automatic leak checking at program exit through a call to _CrtDumpMemoryLeaks and generate an error report if the application failed to free all the memory it allocated. OFF: Don't automatically perform leak checking at program exit.
#endif

	engine_test test{};

	if (test.initialize())
	{
		test.run();
	}

	test.shutdown();
}