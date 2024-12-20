# Projet Total War :boom:

## Membres du groupe 🤵

- Elouan Barois
- Fabien Devillechabrolle
- Victor Gaspard
- Thomas Ploix

## Réflexion stratégique et tactique 🤔
Nous voulions un combat vraiment épique digne d'un film d'action. L'idée d'une **bataille sur 4 fronts** nous paraissait chaotique et passionante.<br/>Nous voulions également que les drones soient assez intelligents pour être capables de garder la meme clible, ne pas faire de déplacements inutiles, et au bout d'un certain temps se concentrer ensemble sur une même cible.

## Développement
### Arbres de comportement 🌳
- Il y a eu plusieurs itérations des arbres de comportement, notamment une où les 4 équipes avaient un arbre personnalisé
- Finalement nous avons opté pour un arbre général pour favoriser la surprise du gagnant
- Celui-ci est assez fidèle à l'arbre d'origine cependant il y a eu des ajouts:
  
  ![image](https://github.com/user-attachments/assets/9659413e-4f37-4cc6-a193-b2a22650de6c)
- Ajout d'un répéteur/séquenceur afin de rester sur la meme cible
- Modification de certaines taches

### Fonctionnalités ajoutées 💡


- Fonctions de sélection d'ennemis:
    - GetFurthestEnemy
    - GetClosestEnemy
  
- Création de squad dirigiés par l'armyManager qui donne des ordres à ses drones 
    - **Attaquer** : les drones se dirigent vers l'ennemi le plus loin
    - **Défendre** : les drones se dirigent vers l'ennemi le plus proche

- Refonte de la fonction seek:
    - **Recalcul** de la trajectoire en cours
    - On évite que les drones se croisent en allant chacun vers leur cible
    - On réduit les mouvements inutiles
    
- Gameplay: 
  - Création de **2 équipes supplémentaires** pour un combat encore plus **épique**
  - Nouvelle classe : **Drone sniper** (unique aux violets) tirant à longue distance, avec un tir unique plus puissant
  - Gestion de gold mannuel qui  permet d'appeler des **renforts** qui apparaissent au château de l'équipe, 100 golds pour faire apparaitre un drone
(l'objectif était de faire donner des golds pour des drones tués)

  - On a retiré les tourelles par soucis de lisibilité

### Scripts C#

Sélection d'ennemis:
```C#
public GameObject GetFurthestEnemy<T>(Vector3 centerPos, float minRadius, float maxRadius) where T : ArmyElement {
        var enemies = GetAllEnemiesOfType<T>(true).Where(
            item => Vector3.Distance(centerPos, item.transform.position) > minRadius
                    && Vector3.Distance(centerPos, item.transform.position) < maxRadius);

        var furthestEnemy = enemies.OrderByDescending(item => Vector3.Distance(centerPos, item.transform.position))
                                   .FirstOrDefault();

        return furthestEnemy?.gameObject;
    }
    public GameObject GetClosestEnemy<T>(Vector3 centerPos, float minRadius, float maxRadius) where T : ArmyElement {
        var enemies = GetAllEnemiesOfType<T>(true).Where(
            item => Vector3.Distance(centerPos, item.transform.position) > minRadius
                    && Vector3.Distance(centerPos, item.transform.position) < maxRadius);

        var closestEnemy = enemies.OrderBy(item => Vector3.Distance(centerPos, item.transform.position))
                                  .FirstOrDefault();

        return closestEnemy?.gameObject;
    }
```

Navigation vers l'ennemi:
```C#
public override TaskStatus OnUpdate()
    {
        //Tache non réussie si la cible n'existe pas ou meurt en cours
        if (target.Value == null)
            return TaskStatus.Failure;

        // Tache réussie si on est proche de la cible finale.
        if (HasArrived())
            return TaskStatus.Success;

        // Recalculer la cible intermédiaire si on est proche de la cible finale.
        if (Vector3.Distance(transform.position, lastDestination) < arriveDistance.Value)
        {
            UpdateIntermediateDestination();
        }

        return TaskStatus.Running;
    }

    private void UpdateIntermediateDestination()
    {
        if (target.Value == null) return;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.Value.position;
        Vector3 directionToTarget = (targetPosition - currentPosition).normalized;

        //Calculer la cible intermédiaire sur le chemin de la cible finales
        Vector3 intermediatePosition = currentPosition + directionToTarget * fractionOfDistance.Value * Vector3.Distance(currentPosition, targetPosition);

        lastDestination = intermediatePosition;
        SetDestination(lastDestination);
            Debug.Log($"Intermediate Position: {intermediatePosition}, Target: {target.Value.name}");

    }

    public override void OnReset()
    {
        base.OnReset();
        target = null;
        fractionOfDistance = 0.5f;
    }
}
```

Création de squad:
```C#
public class CreatSquad : Action
{
    ArmyManager _armyManager;
    private List<ArmyElement> allies;

    
    public override void OnAwake()
    {
        _armyManager =(ArmyManager) GetComponent(typeof(ArmyManager));
    }

    public override TaskStatus OnUpdate()
    {
        if (_armyManager == null) {
            Debug.Log("ArmyManager is null");
            return TaskStatus.Running; // reference to the ArmyManager has not been injected yet
        }
        allies = _armyManager.GetAllAllies(false);
         var nbrAllies = allies.Count;
         int moitier = nbrAllies / 2;
        if (nbrAllies > 0)
        {
            _armyManager.squad1 = allies.GetRange(0, moitier);
            _armyManager.squad2 = allies.GetRange(moitier, moitier);
            Debug.Log("Squad 1: " + _armyManager.squad1.Count + " Squad 2: " + _armyManager.squad2.Count);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
```

## Remarques

  ### Répartition de la production
**Elouan** s'est occupé d'améliorer l'**IA des drones** via leur arbre de comportement, et de créer 2 équipes supplémentaires.

**Fabien** s'est occupé de créer le **rôle sniper** qui est un drone tirant à longue distance avec un seul projectile puissant, et de rajouter du décor naturel au champ de bataille.

**Victor** s'est occupé de transformer les drones en **magiciens** lanceurs d'orbes magiques et d'améliorer les graphismes.

**Thomas** s'est occupé d'ajouter le **gameplay des châteaux et de golds** faisant apparaître des drones supplémentaires et a rajouter le comportement des escouades de drones et l'ordre donner depuis l'armyManager.


## Statistique

A retrouver dans le fichier `Statistique.csv`

    Timer	DroneRouge	DroneVertJaune	DroneViolet	DroneMarronBleu	Vainqueur
    45	0	0	9	0	Violet
    60	5	0	0	0	Rouge
    55	0	1	0	0	Vert
    39	0	0	10	0	Violet
    53	0	5	0	0	Vert
    43	0	0	1	0	Violet
    48	0	0	6	0	Violet
    43	0	5	0	0	Vert
    50	0	5	0	0	Vert
    51	0	0	4	0	Violet
    49	0	0	4	0	Violet
    40	0	4	0	0	Vert
    50	0	0	7	0	Violet
    50	3	0	0	0	Rouge
    55	0	0	4	0	Violet
    47	0	0	5	0	Violet
    58	0	3	0	0	Vert
    50	0	4	0	0	Vert
    50	0	0	0	5	Bleu
    43	0	0	9	0	Violet