pipeline {
    agent any

    environment {
        DOTNET_ROOT = "/usr/share/dotnet"
        DOTNET_CLI_HOME = "${env.WORKSPACE}"
        DEPLOY_PATH = "/var/www/backend"
    }

    stages {
        stage('Clone') {
            steps {
                git branch: 'master', url: 'git@github.com:vivek476/SSJPSAPI.git'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish SSJPSAPI/SSJPSAPI.csproj -c Release -o published'
            }
        }

        stage('Deploy') {
            steps {
                sh 'sudo pkill -f "dotnet ${DEPLOY_PATH}/SSJPSAPI.dll" || true'
                sh 'sudo rm -rf ${DEPLOY_PATH} && sudo mkdir -p ${DEPLOY_PATH}'
                sh 'sudo cp -r published/* ${DEPLOY_PATH}/'
                sh 'nohup dotnet ${DEPLOY_PATH}/SSJPSAPI.dll --urls=http://0.0.0.0:5269 &'
            }
        }
    }
}
