pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        powershell 'dotnet publish DaedalusBackup.API'
      }
    }
    stage('Test') {
      steps {
        powershell 'dotnet test BackupManagement.UnitTests'
      }
    }
  }
}