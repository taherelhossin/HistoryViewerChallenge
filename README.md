# Chat Room History Viewer

This simple app aim to provide simple architecture of a chat room viewer Lib,
Also this app has build to be extendable, so you can add your data provider or applying different algorithms for getting data  very easy by follow the contracts and inject your implementation.

### Note
	 

 - This app is not fancy in the client layer, it's main purpose just to
   display data humanly.

	 
	

 - The data manager built as stateful, but as I said above it's very
   simple to implement new one and make it stateless for **Web API**
   usage

# Files
### Contracts

 - **IHistoryDataProvider**: Contract for all data provider to retrieve the data from any data source
 
 - **IHistoryDataManager**: Contract for all data manipulation implementations
			
### Implementation
 - **InMemoryHistoryDataProvider**: Act as data provide for in memory History data.
 - **HistoryDataStatefulManager** : Default implementation for `IHistoryDataManager`

# Tests

Tests in this Solution focuse primary to provide a POC that the code is easily testable more than testing the extreme corner cases

# Future improvments

One of the improvements I think if added will give this app more extensibility which is extracting the actions **beautifier** to be **injectable** rather than tide to the manager, In this case, we will be able to support different scenarios like Multi-lingual, other date formats & more fancy properties and ideas.

This refactoring will lead the  **beautifier** itself to be Testable, so it will not be tested inside the manager but will be testable isolatted 

