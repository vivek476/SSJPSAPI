pipeline {
    agent any

    environment {
        DOTNET_ROOT = '/home/ubuntu/.dotnet'
        PATH = "/home/ubuntu/.dotnet:/home/ubuntu/.dotnet/tools:${env.PATH}"
    }

    stages {
        stage('Restore') {
            steps {
                sh '''
                    echo Using dotnet version:
                    chmod +x /home/ubuntu/.dotnet/dotnet
                    dotnet --version
                    dotnet restore
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
                sh 'dotnet publish -c Release -o out'
            }
        }

        stage('Deploy') {
            steps {
                sh '''
                    echo Deploy your app here...
                '''
            }
        }
    }
}

