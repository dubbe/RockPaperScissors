# RockPaperScissors
Rock paper scissor backend built in asp.net core

## Usage

### Prerequisites
* Dotnet.core 2.2 (https://dotnet.microsoft.com/download/dotnet-core/2.2)


### Start the application
The program should run in any dotnet core environment.

```bash
cd src/RockPaperScissors
dotnet run
```

The program is running on https://localhost:5001

### Start a game
**Curl command**
```bash
curl -k -d '{"name":"first_player"}' -H "Content-Type: application/json" -X POST https://localhost:5001/api/games
```

**Return example**
```
"01578a05-f904-4b84-a86a-7cfb83b812a8"
```

It will return a string with an id that is needed to play the game. Send the id to your future opponent and remember it for when it is your turn to play.

> *```-k``` is used to allow an unsigned certificate, just for testing purposes!*

### Join an existing game
**Curl command**
```bash
curl -k -d '{"name":"second_player"}' -H "Content-Type: application/json" -X POST https://localhost:5001/api/games/{id}/join
```

**Return example**
```json
{
    "id":"01578a05-f904-4b84-a86a-7cfb83b812a8",
    "status":"Waiting for any player to play",
    "players":[
        {
            "name":"thomas",
            "status":"Has not made a move"
        },
        {
            "name":"second_player",
            "status":"Has not made a move"
        }
    ]
}
```

It will return the status of the current game. Have your opponent already made their move?

### Make your move
It is possible to make your move even before the other player has joined the game, and don't worry. There is no peeking.

**Curl command**
```bash
curl -k -d '{"name":"first_player", "move": "rock"}' -H "Content-Type: application/json" -X POST https://localhost:5001/api/games/{id}/move
```

**Return example**
```json
{
    "id":"01578a05-f904-4b84-a86a-7cfb83b812a8",
    "status":"Waiting for second player to play",
    "players":[
        {
            "name":"thomas",
            "status":"Has made a move"
        },{
            "name":"second_player",
            "status":"Has not made a move"
        }
    ]
}
```

> *Allowed moves are ```Rock```, ```Paper``` and ```Scissors```*

### Check who won
So, both players made their moves? Time to see who won!

**Curl command**
```bash
curl -k -i -H "Accept: application/json" -H "Content-Type: application/json" -X GET  https://localhost:5001/api/games/{id}
```
**Return example**
```json
{
    "id":"01578a05-f904-4b84-a86a-7cfb83b812a8",
    "status":"Game finished",
    "players":[
        {
            "name":"thomas",
            "status":"Played Rock and won the game"
        },{
            "name":"second_player",
            "status":"Played Scissors and lost the game"
        }
    ]
}
```

## Tests

### Prerequisites
* xunit
* xunit.runner.visualstudio

In ```/src/RockPaperScissorsTests``` are all the tests. There is both integration-tests and unit-tests, all written in xunit.

### Run the tests
```bash
cd src/RockPaperScissorsTests
dotnet test
```

## Contributing
1. Fork it (https://github.com/dubbe/RockPaperScissors)
2. Create your feature branch (git checkout -b feature/fooBar)
3. Commit your changes (git commit -am 'Add some fooBar')
4. Push to the branch (git push origin feature/fooBar)
5. Create a new Pull Request