pipeline{
    
    agent any
    environment {
        registry = "excellerentsolutions/eppbe"
        registryCredential = 'dockerhubID'
        eppbeImage = ''
        IMAGE_TAG = "excellerentsolutions/eppbe:$BUILD_ID"
        
    }
    
    stages
    { 
        stage('Git checkout')
        {
            steps{
              git credentialsId: 'jenkins-bitbucket-omeseret', url: 'https://bitbucket.org/Excellerent_Solutions/excellerent-epp-be'
        
            }
        }
        stage('Dotnet build')
        {
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
               sh 'dotnet build' 
            }
        }
        stage('Dotnet test')
        {
            agent{
             docker
                {
                 image 'mcr.microsoft.com/dotnet/sdk:5.0'
                  args '-u root:root'
                }
            }
            steps{
              sh 'dotnet test'  
            }
        }
        stage('Deploy to Staging')
        {
            when {
                branch 'master'
            }
            steps{
                script 
                {
                
                 
                    sshagent(credentials : ['dev']) {
                        eppbeImage = docker.build registry + (":$BUILD_ID")
                        docker.withRegistry( '', 'dockerhubID' )
                            {
                             eppbeImage.push()
                                 
                              sh "ssh -o StrictHostKeyChecking=no ubuntu@3.138.163.97 docker stop eppbe || true"
                              sh "ssh -o StrictHostKeyChecking=no ubuntu@3.138.163.97 docker rm eppbe || true"
                                            
                              sh "ssh -o StrictHostKeyChecking=no ubuntu@3.138.163.97 docker pull  ${IMAGE_TAG} "
                              sh "ssh -o StrictHostKeyChecking=no ubuntu@3.138.163.97  docker run -d -p 3030:80 --name eppbe  ${IMAGE_TAG}"                }
                            }
                      
                    }
              
            }     
                 
            
        }
    
    }
}
