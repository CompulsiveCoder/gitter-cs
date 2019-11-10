pipeline {
    agent any
    options {
        skipDefaultCheckout true
    }
    stages {
        stage('CleanWSStart') {
            steps {
                deleteDir()
            }
        }
        stage('Checkout') {
            steps {
                shHide( 'git clone -b $BRANCH_NAME https://${GHTOKEN}@github.com/CompulsiveCoder/gitter-cs.git .' )
                sh 'git config --global user.email "compulsivecoder@gmail.com"'
                sh 'git config --global user.name "CompulsiveCoderCI"'
            }
        }
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
        stage('Graduate') {
            steps {
                sh 'sh graduate.sh'
            }
        }
        stage('CleanWSEnd') {
            steps {
                deleteDir()
            }
        }
    }
    post {
        always {
            cleanWs()
        }
    }
}
def shHide(cmd) {
    sh('#!/bin/sh -e\n' + cmd)
}

