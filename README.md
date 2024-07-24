# Mimic

### Инициализация client:

1. Сделать сертификаты для https для разарботки фронта  
   1.1. Устанавливаем mkcert  
   1.2. В папке `.\WebClient\dev_certificates` выполнить команды:
   ```
   mkcert -install
   mkcert localhost
   ```
2. Запуск клиента  
   2.1. Обновляем пакеты перед первым запуском. `npm install`  
   2.2. Запуск `npm run dev`  

### PS

Запуск бека с авторизацией через профиль "http-auth"
