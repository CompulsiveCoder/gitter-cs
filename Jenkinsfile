pipeline {
    agent any
    triggers {
        pollSCM 'H/2 * * * *'
    }
    stages {
<<<<<<< HEAD
=======
        stage('Checkout') {
            steps {
                checkout scm

                sh "git config --add remote.origin.fetch +refs/heads/master:refs/remotes/origin/master"
                sh "git fetch --no-tags"
                sh 'git checkout $BRANCH_NAME'
                sh 'cp /usr/local/jenkins/set-gittercs-git-credentials.sh set-gittercs-git-credentials.sh'
                sh 'sh set-gittercs-git-credentials.sh'
            }
        }
>>>>>>> dev
        stage('Init') {
            steps {
                sh 'sh init.sh'
            }
        }
        stage('Build') {
            steps {
                sh 'sh build.sh'
            }
        }
        stage('Test') {
            steps {
                sh 'sh test.sh'
            }
        }
<<<<<<< HEAD
=======
        stage('Graduate') {
            steps {
                sh 'sh graduate.sh'
            }
        }
        stage('Clean') {
            steps {
              cleanWs()
            }
        }
>>>>>>> dev
    }
    post {
        always {
            cleanWs()
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> dev
