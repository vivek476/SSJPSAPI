pipeline {
    agent any

    triggers {
        githubPush() // Trigger on push
    }

    environment {
        EC2_IP = '34.230.40.32' // Public IP of EC2
        SSH_CRED = 'cf0fd30a-c4e7-4017-a7b5-f7cd712eb3c5' // Jenkins SSH credential ID
        APP_DIR = '/home/ubuntu/backend-app'
        GIT_REPO = 'https://github.com/vivek476/SSJPSAPI.git'
        APP_NAME = 'backend-api' // PM2 app name
    }

    stages {
        stage('Deploy Backend API using PM2') {
            steps {
                sshagent (credentials: [SSH_CRED]) {
                    sh """
                    ssh -o StrictHostKeyChecking=no ubuntu@${EC2_IP} '
                        echo "Installing PM2 globally"
                        sudo npm install -g pm2

                        echo "Cloning fresh repo"
                        rm -rf ${APP_DIR}
                        git clone ${GIT_REPO} ${APP_DIR}
                        cd ${APP_DIR}
                        npm install

                        echo "Restarting app using PM2"
                        pm2 delete ${APP_NAME} || true
                        pm2 start index.js --name ${APP_NAME}
                        pm2 save
                        pm2 startup systemd -u ubuntu --hp /home/ubuntu
                    '
                    """
                }
            }
        }
    }
}
