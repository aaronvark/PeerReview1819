# PeerReview1819

Repository for second year students of Game Development at the HKU University of the Arts, Utrecht.

## How to use this repository

### Getting the repository on your local machine
Before you can start adding things to the repository, you'll need to setup a git environment on your own machine:

	git init
	git remote add origin https://github.com/aaronvark/PeerReview1819.git
	git pull origin master

### Issues
The most important thing to remember is to **always have an issue that you can reference**. To create your first issue, go to the **Issues** tab, and add a new issue. Follow the format of the example issue: **name - task**. Remember the number that's now behind your issue (you will need to know this to reference it)

### Git Flow
The next thing to know is how to use **git flow**, a way of working around a master repository without pushing to it directly. The steps taken to add code to the repository are as follows:

- pull the master branch to your local machine
- create a branch (for example with your name) for the code you want to add
- commit to the branch while '''referencing''' (#) your issue number
- push the branch to the repository

These steps are done on the command line as follows:

    git pull origin master
    git branch mynewbranchname      (instead of mynewbranchname, for example use your own name)
    git commit -m "re #1, I did some amazing work!"      (instead of #1, use your own issue number)
    git push origin mynewbranchname** (again, use the branch name you came up with)

Once you've gotten this far, your code is committed to the online repository, but it's not yet on the master branch. In order for it to get there, you have to **request that somebody pull it**, which is called a **Pull Request**. You can create a pull request from the browser:

- 