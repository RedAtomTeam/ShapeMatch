# ShapeMatch
Простая пазл-игра, где цель игрока состоит в очистке игрового поля от появившихся фигур.

![Screenshot2](https://sun9-64.userapi.com/impg/qVHtdtCCV_q84UkRemPgYgTLQojFjjQvmFdVfg/KE6wgS0hegY.jpg?size=434x892&quality=95&sign=45d1fd70c7ee28dc534dcb6620b90529&type=album)

## 🎮 Геймплей
Со стартом уровня на игровое поле сыпятся фигурки с разной формой, цветом и картинкой.
Задача игрока состоит в том, чтобы очистить поле от всех фигурок, отправляя их в 
экшен-бар. 

Одинаковые фигурки исчезают из экшен-бара если выстраиваются по три в ряд. 
Экшен-бар имеет ограниченную вместимость и при заполнении экшен-бара игрок проигрывает. 

В игре присутствует 18 уровней.

![Screenshot2](https://sun9-6.userapi.com/impg/RP2DRZKht9Qi6sGy9qlavUUEEx2a15h4-BFHOw/bN7ZGm1TbVw.jpg?size=431x885&quality=95&sign=d21efb4bf2d3db64ab5196ba4a8fbf17&type=album)
![Screenshot2](https://sun9-47.userapi.com/impg/j2awjNqMxY4Vtv5hCTWT3HyVUpC9hP_Z-5bnHA/uP1eB18TmnU.jpg?size=429x885&quality=95&sign=2ca4c68356ff4e74662769971a19be33&type=album)


## 🛠 Технические аспекты разработки
### Платформа
WebGL - игра размещена на веб-платформе Yandex Games и работает для мобильных устройств

https://yandex.com/games/app/442255?lang=ru

### Стек
Движок: Unity

Packages: DOTween, YG Plugin(1.6)

### Вся работа приложения организована через систему скриптов:
- Piece - Базовая фигурка на поле. За внешний вид и управление им отвечает класс Shape.
- PieceSpawner - Отвечает за спавн и перемешку фигурок(Piece) на поле.
- ActionBar - Экшенбар на уровне. Имеет настройки вместимости и длинну последовательности для удаления.
- ActionBar_Element - Элемент экшенбара, отвечающий за визуализации содержимого каждой ячейки.
- AudioService - Сервис управления саундтреком и звуковыми эффектами.
- LevelsConfig - SO для хранения информации об уровнях.
- Так же присутствует несколько классов отвечающих за управление UI.


## 💬 Контакты
- Почта: redatomteam@gmail.com
- ТГ: @gennady_a1
