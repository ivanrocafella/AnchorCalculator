#!/bin/sh

echo "Ожидание MySQL..."
max_attempts=30  # 60 секунд (30 попыток по 2 секунды)
attempt=1

while ! mysqladmin ping -h mysql -u IvanKobtsev -p 901321Kobcev --silent; do
    if [ $attempt -ge $max_attempts ]; then
        echo "Ошибка: MySQL не стал доступен после $max_attempts попыток. Выход."
        exit 1
    fi
    echo "Попытка $attempt: MySQL недоступен, ждем 2 секунды..."
    sleep 2
    attempt=$((attempt + 1))
done

echo "MySQL запущен. Запуск приложения..."
exec "$@"
