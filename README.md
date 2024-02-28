# SocialTree Backend

This repository contains the backend code for the SocialTree application. The backend is split into two separate projects:

1. **Authorization Server**: Responsible for handling user authentication and token generation.
2. **Main Server**: Provides APIs for various functionalities such as managing posts, likes, relations, etc.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 8.0.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (optional)

### Installation

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/ArzitPanda/PostDotnet.git
   ```

2. Navigate to the `AuthserverSlack` and `slackApi` directories respectively and build the projects:

   ```bash
   cd AuthserverSlack
   dotnet build
   
   cd ../MainServer
   dotnet build
   ```

### Running the Servers

1. Start the Authorization Server:

   ```bash
   cd AuthserverSlack
   dotnet watch
   ```

2. Start the Main Server:

   ```bash
   cd slackApi
   dotnet watch
   ```

###  SettingUp Frontend CallBack
 - once set the project can change the redirecturl is present in the slackApi authcontroller ,you can use your 


### Accessing Swagger UI

- Once the servers are running, you can access the Swagger UI for the Main Server at [http://localhost:5283/swagger](http://localhost:5283/swagger).
- Use the provided endpoints to interact with the backend APIs.

## Endpoints

### Authorization Server

- **POST /api/Auth/signup**: Sign up a new user.
- **GET /api/Auth/login**: Log in and generate an access token.
- **GET /api/Auth/callBack**: Callback endpoint for OAuth authentication.

### Main Server

- **GET /api/Feed**: Retrieve the feed.
- **POST /api/Like**: Like a post.
- **GET /api/Like/{postId}**: Get likes for a post.
- **DELETE /api/Like/{userId}/{postId}**: Unlike a post.
- **GET /api/Post**: Get all posts.
- **POST /api/Post**: Create a new post.
- **GET /api/Post/{id}**: Get a specific post.
- **PUT /api/Post/{id}**: Update a post.
- **DELETE /api/Post/{id}**: Delete a post.
- **GET /api/Post/person/{personId}**: Get posts by a person.
- **GET /api/Post/person/{personId}/visibility/{visibility}**: Get posts by a person with specific visibility.
- **POST /api/Relation**: Create a relation.
- **PUT /api/Relation**: Update a relation.
- **GET /api/Relation/{relationId}**: Get a specific relation.
- **GET /api/Relation/sender/{senderId}**: Get relations sent by a person.
- **GET /api/Relation/receiver/{receiverId}**: Get relations received by a person.
- **POST /api/RelationRequest**: Send a relation request.
- **GET /api/RelationRequest**: Get all relation requests.
- **GET /api/RelationRequest/{id}**: Get a specific relation request.
- **PUT /api/RelationRequest/{id}**: Update a relation request.
- **DELETE /api/RelationRequest/{id}**: Delete a relation request.
- **GET /api/RelationRequest/requestor/{id}**: Get relation requests sent by a person.
- **GET /api/User/{id}**: Get user details.
- **PUT /api/User/{id}**: Update user details.
- **DELETE /api/User/{id}**: Delete a user.
- **POST /api/User**: Create a new user.

## Contributing

Feel free to contribute to this project by submitting bug reports, feature requests, or pull requests. 

## License

This project is licensed under the [MIT License](LICENSE).
