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

# About the API Solution
    - The folder PlanADevOpsChallenge1API contains all the code related to the API.

# About docker packaging
    - dockerfile can be found at the root level.
    - The latest stable version of the API image is located at this public repo: registry-1-stage.docker.io/gdcorbijn/planadevopschallenge1repo:latest                 