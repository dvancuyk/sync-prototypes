# sync

This project outlines the basic ideas I'm working on to make our Sync Process both faster and more reliable.

## Branch Definitions
* Main - Most current branch of the code.
* Phase 1 - focuses on testing whether we should save collections using TVPS or saves of individual items
* Phase 2 - Focuses on testing whether we should have a single proc that uses merge code or individual stored procs for delete, update, and inserts


## Current Notes
TVP and save collections are the way to go. TVPs are quicker.

Changes: 
 -> Change repository to do so at the end

Tests:
 -> Determine if using separate calls for Insert, Delete, and Update is faster or slower than using the TVP methodology
     -> Determine if parallel tasks are running, how each is affected.
 -> Determine if using subsets of changed values is faster or slower than passing in all of the values