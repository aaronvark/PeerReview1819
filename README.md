# PeerReview1819

Repository for second year students of Game Development at the HKU University of the Arts, Utrecht.

## How to use this repository

### Getting the repository on your local machine
Before you can start adding things to the repository, you'll need to setup a git environment on your own machine.

- **Windows**, I recommend Git for Windows: https://git-scm.com/download/ (and then right click in folders and choose **Git Bash Here**)
- **Mac**, You can use the **Terminal**. Check if you have it using **git --version**, follow instructions if you don't

Once you have Git on your machine, use the following command line commands:

	git init
	git remote add origin https://github.com/aaronvark/PeerReview1819.git
	git pull origin master
	
	//if you've already created a branch before
	git fetch origin yourbranchname
	git checkout yourbranchname

There are GUI tools for git, but I suggest learning to use the command line to become more familiar with the individual commands that git requires you to be familiar with. GUI tools take a lot of the thinking out of your hands, so if you learn to use the commands yourself, you will (eventually) understand it better.

### Creating Issues
The most important thing to remember is to **always have an issue that you can reference**. To create your first issue, go to the **Issues** tab, and add a new issue. Follow the format of the example issue: **name - task**. Remember the number that's now behind your issue (you will need to know this to reference it)

Issue guidelines:
1. Always have an issue up to work on (for example: "Eindopdracht Blok 1")
2. Create an issue you can work on for multiple commits / pull requests, **that is relevant to the course you're in**
3. Always reference your issue number in commits / pull requests
4. Close your issue when you've finished
5. Never close somebody else's issues

### Git Flow
The next thing to know is how to use **git flow**, a way of working around a master repository without pushing to it directly. The steps taken to add code to the repository are as follows:

- pull the master branch to your local machine
- create a branch (for example with your name) for the code you want to add (you can use the same branch more than once)
- commit to the branch while '''referencing''' (#) your issue number
- push the branch to the repository

These steps are done on the command line as follows:

    	//if you DON'T already have a branch
	git checkout master
	git pull origin master
    	git branch mynewbranchname              (instead of mynewbranchname, for example use your own name)
	git checkout mynewbranchname
	
	//if you DO already have a branch
	git checkout mybranchname
	git pull origin master			(this will make your branch "up to date" with the remote master)
	
	[do some work]
	
	git add myfoldername/.								   (. adds all the files in the folder called "myfoldername")
    git commit -m "re #1, I did [work] for [reason]!"      (instead of #1, use your own issue number)
    git push origin mynewbranchname                        (again, use the branch name you came up with)

Once you've gotten this far, your code is committed to the online repository, but it's not yet on the master branch. In order for it to get there, you have to **request that somebody pull it**, which is called a **Pull Request**. You can create a pull request from the browser:

- go to the repository page: http://github.com/aaronvark/PeerReview1819/
- go to your own branch
- if it **does not say** that your branch is "x commits ahead of master", follow the **pulling master into branch** first
- click the "Pull Request" button to the right
- confirm that it looks like [master] < [yourbranch]
- add a title and description
- click **Create Pull Request**

Somebody will have to **review** your pull request before it is **merged** into the master branch. If you started a new issue, and want to make a new pull request, and your current one has not been merged, **ask somebody to review it** before you continue. If you commit after doing a pull request, the later commit (on the same branch) will be added to the pull request (so it only matters when you are working on a different issue).

#### Pulling master into branch
If the master branch has been updated while you were working on your branch, you will have to pull the master branch into your branch before continuing. While you are **on your own branch**, do the following:

If you want to check on which branch you are:

	git status

And if you want to move to your own branch:

	git checkout mybranchname

Finally, pulling the updated master into your own branch:

	git pull origin master

Then, you can push the newly merged branch to the repository:

	git push origin mybranchname



