pipeline {
    agent any
    environment {
        DOTNET_ROOT = "/opt/dotnet"
        PATH = "/opt/dotnet:$PATH"
    }
    stages {
        stage('Restore') {
            steps {
                sh '''
                    echo Using dotnet version:
                    dotnet --version
                '''
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish -c Release -o published-app'
            }
        }

        stage('Deploy') {
            steps {
                sh 'echo Deploy your app here'
            }
        }
    }
}

