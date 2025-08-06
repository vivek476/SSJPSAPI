pipeline {
    agent any

    environment {
        DOTNET_ROOT = "/usr/share/dotnet"
        DOTNET_CLI_HOME = "${env.WORKSPACE}"
        DEPLOY_PATH = "/var/www/backend"
    }

    stages {
        stage('Install .NET SDK') {
            steps {
                sh '''
                    if ! command -v dotnet &> /dev/null
                    then
                        echo "Installing .NET SDK..."
                        wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
                        sudo dpkg -i packages-microsoft-prod.deb
                        sudo apt-get update
                        sudo apt-get install -y apt-transport-https
                        sudo apt-get install -y dotnet-sdk-8.0
                    else
                        echo ".NET SDK already installed"
                    fi
                '''
            }
        }

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
                sh '''
                    sudo pkill -f "dotnet ${DEPLOY_PATH}/SSJPSAPI.dll" || true
                    sudo rm -rf ${DEPLOY_PATH} && sudo mkdir -p ${DEPLOY_PATH}
                    sudo cp -r published/* ${DEPLOY_PATH}/
                    nohup dotnet ${DEPLOY_PATH}/SSJPSAPI.dll --urls=http://0.0.0.0:5269 &
                '''
            }
        }
    }
}

