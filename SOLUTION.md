## Task 1
The instructions for getting started didn't mention anything about setting up the DB, so it crashed when creating a new user. I ran the command "dotnet ef database update" from the \tech-test-netcore\Todo folder to set up the DB, and then was able to use the site.

*Note* By having candidates fork your repo, it's possible for us to see the repos that others have created before us. When forking there is a link to all other forks, which would make it easy to cheat.


## Task 4
The task didn't specify what the "friendly" text should be. Given that the code was using ResponsiblePartyId I worked on the assumption that "Responsible Party" would be an understood term, like the whole concept of the ubiquitous language in DDD, but I did also consider putting something a little less formal like "Task Owner" as well. In a real world scenario I might have developed a better feel for the product & audience to know what to use, or if not would check with a product owner or similar if wording was important.

## Task 5
This spec feels quite vague. Would we just want the hiding to be in the context of the current view, in which case a Javascript-based approach would be optimal to keep it all in the browser. Might we want each list to remeber the state, or have a setting at the user level, if so then we'd want to extend the appropriate data models to persist this. My solution here is somewhere inbetween, so it isn't optimal, but I am at least trying to consider the alternatives were I working on some production code.

## Task 6
This change hits the requirement as specified, but I'm thinking that for a production-ready system we'd need to treat lists created by other users differently. Would we be able to see all of the other tasks that aren't assigned to us at all, or might we just get a read-only view of the other tasks, and only be allowed to alter the one that we've been given?

## General note
I like to take a TDD approach to writing code, but the changes here have been very UI-centric, or in the call to database. I'm taking a pragmatic approaching and going with the flow of the code that's already here, but I'd be thinking about how I could add some integration tests, or put in some more structure to the unit tests that would help them look at the sort of changes that are being made if this were a production codebase.


