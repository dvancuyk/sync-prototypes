
# sync

This project outlines the basic ideas I'm working on to make our Sync Process both faster and more reliable.

## Branch Definitions
* Main - Most current branch of the code.
* Phase 1 - focuses on testing whether we should save collections using TVPS or saves of individual items
* Phase 2 - Focuses on testing whether we should have a single proc that uses merge code or individual stored procs for delete, update, and inserts
* Phase 3 - Focus on whether a single stored proc with 3 table parameters is better performaning than a single stored proc using a MERGE statement

## Current Notes
TVP and save collections are the way to go. TVPs are quicker.

## Changes: 
 * Change repository to do so at the end

## Tests:

* Determine if using separate calls for Insert, Delete, and Update is faster or slower than using the TVP methodology
     - Determine if parallel tasks are running, how each is affected.
* Determine if using subsets of changed values is faster or slower than passing in all of the values. __Subsets are faster. See phase 3 testing__
* Determine how to schedule different components. 
	- What would the schedule payload look like?
	- How would the scheduler load the components scheduled to run?
	- How would the scheduler retain scheduled tasks that have run?
	- Is there a way we can do all of this without having class definition exposion?
	- Make it resilient.  It needs to do the following things:
		1. Load from server when available
		2. Load from local when server is unavailable
		3. Have a default value for all customers and be able to override for a specific customer.
