pipeline {
    agent any

    triggers {
        githubPush()
    }

    environment {
        EC2_IP = '34.230.40.32'
        SSH_CRED = 'cf0fd30a-c4e7-4017-a7b5-f7cd712eb3c5'
        APP_DIR = '/home/ubuntu/backend-app'
        GIT_REPO = 'https://github.com/vivek476/SSJPSAPI.git'
        DOTNET_PROJECT_PATH = '/home/ubuntu/backend-app/SSJPSAPI'
        PUBLISH_DIR = '/home/ubuntu/published-api'
    }

    stages {
        stage('Deploy .NET Core App') {
            steps {
                sshagent (credentials: [SSH_CRED]) {
                    sh """
                    ssh -o StrictHostKeyChecking=no ubuntu@${EC2_IP} '
                        echo "Setting environment variables for .NET 8..."
                        export DOTNET_ROOT=\$HOME/.dotnet
                        export PATH=\$PATH:\$HOME/.dotnet

                        echo "Removing old app..."
                        rm -rf ${APP_DIR} ${PUBLISH_DIR}

                        echo "Cloning repo..."
                        git clone ${GIT_REPO} ${APP_DIR}
                        
                        echo "Building .NET project..."
                        cd ${DOTNET_PROJECT_PATH}
                        dotnet clean
                        dotnet build --configuration Release
                        dotnet publish -c Release -o ${PUBLISH_DIR}

                        echo "Stopping existing app if running..."
                        pkill -f "dotnet ${PUBLISH_DIR}/SSJPSAPI.dll" || true

                        echo "Starting app in background..."
                        nohup dotnet ${PUBLISH_DIR}/SSJPSAPI.dll > /home/ubuntu/app.log 2>&1 &
                    '
                    """
                }
            }
        }
    }
}

