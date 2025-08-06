pipeline {
    agent any

    environment {
        DOTNET_ROOT = "/home/ubuntu/.dotnet"
        PATH = "${env.PATH}:${DOTNET_ROOT}:${DOTNET_ROOT}/tools"
    }

    stages {
        stage('Restore') {
            steps {
                sh '''
                    echo "Using dotnet version:"
                    dotnet --version
                    dotnet restore
                '''
            }
        }
        stage('Build') {
            steps {
                sh '''
                    dotnet build
                '''
            }
        }
        stage('Publish') {
            steps {
                sh '''
                    dotnet publish -c Release -o out
                '''
            }
        }
        stage('Deploy') {
            steps {
                echo "Deploy your project here..."
            }
        }
    }
}

