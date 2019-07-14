# DaedalusBackup
DaedalusBackup is a cross-platform virtual machine backup solution for all the main hypervisors. DaedalusBackup is designed from the start to be customizable and easy to integrate into custom solutions as well. Can backup an entire virtual machine, or go incrementally with built-in deduping.

### Roadmap
- Api working with HyperV
    - Full Backups
    - Incremental Backups
    - CIFS for location types
- Web GUI for scheduling backup jobs
- Authentication
- Add NFS to location types
- Add SFTP to location types
- Add KVM or VirtualBox support
- Add VirtualBox or KVM support (depending on what was done above)
- Add ESXI support
- Authorization (roles/permissions)

### GUI
By default, the GUI is [DaedalusBackupDashboard](https://github.com/Capt-Lance/DaedalusBackupDashboard). The intent is to bundle this with each release. Since DaedalusBackup exposes a REST API for interacting with it, creating custom interfaces to interact with it should be relatively easy.

### ESXI
Why is ESXI last? To backup to ESXI, one needs to have the full license (not the free license) that allows use of the backup API. Many students and testers have access to HyperV, KVM, and VitualBox since they are either free, or they get free licenses due to being a student or using Windows 10. If this gets a lot of support, then the priority for ESXI can be rearranged. Most likely swapped out with VirtualBox.