# CakeShop - "KarmeLove"

<p>
    <img src="https://user-images.githubusercontent.com/95192948/187280933-ef5c0536-1ec9-4794-ab13-e250562f4645.png"  alt="CakeShop "KarmeLove"><br>
</p>
<b> Techstack: </b>

<i>Backend:</i>
<li>.NetCore 3.1</li>
<li>Entity framework Core</li>
<li>ActiveMQ - Artemis Net Client </li>
<li> AspNetCore identity</li>
<li>SQL Server </li>
<i>Frontend:</i>
<li>HTML</li>
<li>CSS</li>
<li>JavaScript</li>
<li>Razor</li> <br>
<br>
                                                                                                                                               
# Main project
In MainProject/CakeShop/ there is everything which is responsible for the main purpose of the app. 
Although if you want to check how whole app works - you also need to use "API" project - how to make it work below.

# API
API endpoints are located in open api documentation at API project:
<p>
  <img src="https://user-images.githubusercontent.com/95192948/187282879-1eb26ccd-8f18-48c0-9bc2-3025503e9e8f.png"  alt="CakeShop "KarmeLove"><br>
</p>

Most endpoints are protected and are not free to use - authentification is required, it has been implemented using JWT tokens. 

# ActiveMQ

Active MQ has been used to log all changes made with api calls - get/post/put etc. It is required to successfully use CakeShop API. 
It has been implemented with Artemis client. How to use it, link below: <br>
https://github.com/Havret/dotnet-activemq-artemis-client <br>
https://activemq.apache.org/components/artemis/ <br>

How to start your client:
Go to bin folder at ActiveMQ directory and run "artemis run" in cmd, result should like that: <br>
<p>
  <img src="https://user-images.githubusercontent.com/95192948/187283711-c4fe6fc2-b19c-4800-966d-645beec2fa86.png"><br>
</p>

