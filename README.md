# amctraining
Reference for AMC Training


1. From an Admin Command Prompt, run the command to enable access (check using `whoami`):  
    ```
      netsh http add urlacl url=http://*:11000/ user=domain\user
    ```