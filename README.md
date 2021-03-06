# PlanADevOpsChallenge1


# Local environment tools used to develop, build and deploy this solution:

    - This setup assumes the following:
        - You are working on a Windows OS (Recommended Windows 10). Linux setup is coming on our next release!... Stay tuned :) 
    - Visual Studio Code (VS Code): https://code.visualstudio.com/Download
        - Make sure to install all helper extensions for a better experience: Kubernetes, C#, Docker and YAML.
    - .NET 6.0 framework: https://dotnet.microsoft.com/en-us/download/dotnet
    - Docker: https://docs.docker.com/desktop/windows/install/
    - Kubectl cmd tool: https://kubernetes.io/docs/tasks/tools/install-kubectl-windows/#install-kubectl-binary-with-curl-on-windows
    - Minikube: https://minikube.sigs.k8s.io/docs/start/
    - In order to interact with the docker client the Docker.DotNet library was installed: https://github.com/dotnet/Docker.DotNet

# About Kubernetes tempaltes
    - K8sTemplates folder contains all templates to deploy both the API and the Load Balancer Service that exposes the API.
    - Service Deployment considerations:
        - Deploy "nginx_loadbalancer_service" if you are on a Cloud Provider environment capable of deploying a LoadBalancer to expose the API or if you are using a local k8s cluster such as minikube that supports this type of Servive. To adquire a public IP to reach the API running your local minikube, you must execute the minukube tunnel: minikube service nginxsvc --url, this will generate and return the required configuration (url and port) assigned to nginx by minicube to externally reach the API.
        - Deploy "nginx_nodeport_service" if you are working on an on-premises environment or if your local k8s cluster does not support LoadBalancer type. Keep in mind that under this case, accesibility to the API remains private and only reachable through other Containers within the same docker network. 

# About communicating with Docker Client
    -Specifically for the requirement of returning the engine version:
        -  When running the API directly on the local workstation, connecting to the docker client has no caveats, but when moving into a container the workstation context is lost so extra configurations has to be performed.
        - For docker build and docker run:
            - Make sure to execute your docker run mounting the docker daemon listener /var/run/docker.sock, as an example:
                docker run -v /var/run/docker.sock:/var/run/docker.sock -d -p 8080:80 registry-1-stage.docker.io/gdcorbijn/planadevopschallenge1repo:latest
            - The reason for this parameter is to enable the docker container to have access to the docker client listener os it can communicate and request the docker version. This is recommended for local development.
        - For kubernetes deployment:
            - Include the docker client listener mount as a VolumeMount and make sure you always set it up as readOnly: true to avoid excessive security risk (since you are giving the containers access to the daemon client that manages the cluster). 
            - The SecurityContext has to include the RunAsGroup ID that is running the docker client at the mount point, at the time of writing this documentation, the deployment points to the Group ID of the local minikube K8s cluster used to test the solution: 999, this is NOT a static value, it may vary from cluster to cluster.   

# About the API Solution
    - The folder PlanADevOpsChallenge1API contains all the code related to the API.

# About docker packaging
    - dockerfile can be found at the root level.
    - The latest stable version of the API image is located at this public repo: registry-1-stage.docker.io/gdcorbijn/planadevopschallenge1repo:latest                 