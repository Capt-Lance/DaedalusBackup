pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh 'dotnet publish DaedalusBackup.API'
      }
    }
    stage('Test') {
      steps {
        sh 'dotnet test BackupManagement.UnitTests'
      }
    }
    stage('Cleanup') {
      steps {
        cleanWs(cleanWhenSuccess: true)
      }
    }
  }
}