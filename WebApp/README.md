Comandos para la publicacion y puesta en local en ngrok

dotnet publish -c Release -o ./publish
cd ./publish
dotnet WebApp.dll --urls "http://0.0.0.0:5000"

ngrok:
sudo pacman -S ngrok
ngrok config add-authtoken tokenDengrok
ngrok http 5000