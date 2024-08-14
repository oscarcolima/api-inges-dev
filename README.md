para compilar usar el siguinte comando
```sh
flutter build web \
--dart-define API=https://apiinglesdev.azurewebsites.net/api \
--base-href /assets3/ 
```
> con el parametro  `--dart-define` definiremos una variable en este caso es API la cual tiene de valor la url de la api a consumir.
>
> En conso de que la web no se aloge en el home raiz usar `--base-href` para indicar cual sera la raiz del proyecto