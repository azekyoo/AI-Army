
# Membres du groupe

- Elouan Barois
- Fabien Devillechabrolle
- Victor Gaspard
- Thomas Ploix

# Réflexion stratégique et tactique
Nous voulions un combat vraiment épique digne d'un film d'action. L'idée d'une bataille sur 4 fronts nous paraissait chaotique et passionante. Nous voulions également que les drones soient assez intelligents garder la meme clible, ne pas faire de déplacements inutiles, et au bout d'un certain temps se concentrer ensemble sur une cible.

# Développement
## Arbres de comportement
- Il y a eu plusieures itérations des arbres de comportement, notamment une où les 4 équipes avaient un arbre personalisé
- Finalement nous avons opté pour un arbre général pour favoriser la surprise du gagnant
- Celui-ci est assez fidèle à l'arbre d'origine cependant il y a eu des ajouts.
- - ![image](https://github.com/user-attachments/assets/9659413e-4f37-4cc6-a193-b2a22650de6c)

## Fonctionnalités ajoutées
- Fonctions de sélection d'ennemi:
    - GetFurthestEnemy
    - GetClosestEnemy
- Refonte de la fonction seek:
    - Recalcul de la trajectoire en cours
    - On évite que les drones se croisent en allant chacun vers leur cible
    - On réduit les mouvements inutiles
    


Gameplay: 
- Création de 2 équipes supplémentaires pour un combat encore plus équipe
- Drone sniper tirant à très longue distance
- Châteaux faisant apparaître des drones supplémentaires lorsque l'équipe a fait suffisamment de kills ( Finalement pas complètement implémentés)
- On a retiré les tourelles par soucis de lisibilité

## Scripts C#

# Remarques

# Répartition de la production
Elouan s'est occupé d'améliorer l'IA des drones via leur arbre de comportement, et de créer 2 équipes supplémentaires.

Fabien s'est occupé de créer le rôle sniper qui est un drone tirant à très longue distance, et de rajouter du décor naturel au champ de bataille.

Victor s'est occupé de transformer les drones en magiciens lanceurs d'orbes magiques et d'améliorer les graphismes.

Thomas s'est occupé d'ajouter le gameplay des châteaux faisant apparaître des drones supplémentaires.

