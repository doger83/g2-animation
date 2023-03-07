#include <iostream>
#include <cmath>
using namespace std;

// Dieser Datentyp deklariert einen Punkt in der zweidimensionalen Ebene
struct Point
{
    int x;
    int y;
    // Konstruktoren
    Point(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    Point()
    {
        x = 0;
        y = 0;
    }
};

// Dieser Datentyp deklariert einen Knoten des Baums
struct Node
{
    Point point;
    int value;
    // Konstruktoren
    Node(Point _point, int _value)
    {
        point = _point;
        value = _value;
    }
    Node()
    {
        value = 0;
    }
};

// Diese Klasse deklariert einen Quadtree und seine 4 Teilbäume
class Quadtree
{
    // Wurzelknoten
    Node *root;
    // Diese Punkte definieren den Bereich des Quadtree
    Point topLeft;
    Point bottomRight;
    // Kindknoten des Quadtree
    Quadtree *topLeftTree;
    Quadtree *topRightTree;
    Quadtree *bottomLeftTree;
    Quadtree *bottomRightTree;

public:
    // Konstruktoren
    Quadtree()
    {
        topLeft = Point(0, 0);
        bottomRight = Point(0, 0);
        root = NULL;
        topLeftTree = NULL;
        topRightTree = NULL;
        bottomLeftTree = NULL;
        bottomRightTree = NULL;
    }
    Quadtree(Point _topLeft, Point _bottomRight)
    {
        root = NULL;
        topLeftTree = NULL;
        topRightTree = NULL;
        bottomLeftTree = NULL;
        bottomRightTree = NULL;
        topLeft = _topLeft;
        bottomRight = _bottomRight;
    }

    // Diese rekursive Funktion fügt dem Quadtree einen Knoten hinzu
    void insert(Node *node)
    {
        if (node == NULL)
        {
            return;
        }

        // Wenn das Gebiet des aktuellen Quadtree den Punkt nicht enthält, Funktion verlassen
        if (!inBoundary(node->point))
        {
            return;
        }

        // Wenn das Gebiet des aktuellen Quadtree den Punkt nicht enthält, Funktion verlassen
        if (abs(topLeft.x - bottomRight.x) <= 1 && abs(topLeft.y - bottomRight.y) <= 1)
        {
            if (root == NULL)
            {
                root = node;
            }
            return;
        }

        // Bedingung für den Teilbaum links unten
        if ((topLeft.x + bottomRight.x) / 2 >= node->point.x)
        {
            // Bedingung für den Teilbaum links unten
            if ((topLeft.y + bottomRight.y) / 2 >= node->point.y)
            {
                if (topLeftTree == NULL)
                {
                    topLeftTree = new Quadtree(Point(topLeft.x, topLeft.y), Point((topLeft.x + bottomRight.x) / 2, (topLeft.y + bottomRight.y) / 2));
                }
                topLeftTree->insert(node);
            }
            // Bedingung für den Teilbaum rechts unten
            else
            {
                if (bottomLeftTree == NULL)
                {
                    bottomLeftTree = new Quadtree(Point(topLeft.x, (topLeft.y + bottomRight.y) / 2), Point((topLeft.x + bottomRight.x) / 2, bottomRight.y));
                }
                bottomLeftTree->insert(node);
            }
        }
        else
        {
            // Bedingung für den Teilbaum rechts oben
            if ((topLeft.y + bottomRight.y) / 2 >= node->point.y)
            {
                if (topRightTree == NULL)
                {
                    topRightTree = new Quadtree(Point((topLeft.x + bottomRight.x) / 2, topLeft.y), Point(bottomRight.x, (topLeft.y + bottomRight.y) / 2));
                }
                topRightTree->insert(node);
            }
            // Bedingung für den Teilbaum rechts unten
            else
            {
                if (bottomRightTree == NULL)
                {
                    bottomRightTree = new Quadtree(Point((topLeft.x + bottomRight.x) / 2, (topLeft.y + bottomRight.y) / 2), Point(bottomRight.x, bottomRight.y));
                }
                bottomRightTree->insert(node);
            }
        }
    }

    // Diese rekursive Funktion sucht einen Knoten
    Node *searchNode(Point point)
    {
        if (!inBoundary(point))
        {
            return NULL;
        }
        if (root != NULL)
        {
            return root;
        }
        // Wenn der untere rechte Teilbaum leer ist
        if ((topLeft.x + bottomRight.x) / 2 >= point.x)
        {
            // Wenn der Knoten im Gebiet für den oberen linken Teilbaum liegt
            if ((topLeft.y + bottomRight.y) / 2 >= point.y)
            {
                if (topLeftTree == NULL)
                {
                    return NULL;
                }
                return topLeftTree->searchNode(point);
            }
            // Wenn der Knoten im Gebiet für den unteren linken Teilbaum liegt
            else
            {
                if (bottomLeftTree == NULL)
                {
                    return NULL;
                }
                return bottomLeftTree->searchNode(point);
            }
        }
        else
        {
            // Wenn der Knoten im Gebiet für den oberen rechte Teilbaum liegt
            if ((topLeft.y + bottomRight.y) / 2 >= point.y)
            {
                if (topRightTree == NULL)
                {
                    return NULL;
                }
                return topRightTree->searchNode(point);
            }
            // Wenn der Knoten im Gebiet für den unteren rechten Teilbaum liegt
            else
            {
                if (bottomRightTree == NULL)
                {
                    return NULL;
                }
                return bottomRightTree->searchNode(point);
            }
        }
    };

    // Prüft, ob der aktuelle Quadtree den Punkt enthält
    bool inBoundary(Point point)
    {
        return (point.x >= topLeft.x && point.x <= bottomRight.x && point.y >= topLeft.y && point.y <= bottomRight.y);
    }
};

// Hauptfunktion die das Programm ausführt
int main()
{
    Quadtree center(Point(0, 0), Point(8, 8));
    Node node1(Point(1, 1), 1);
    Node node2(Point(2, 5), 2);
    Node node3(Point(7, 6), 3);
    center.insert(&node1);
    center.insert(&node2);
    center.insert(&node3);
    cout << "Knoten 1: " << center.searchNode(Point(1, 1))->value << "\n";
    cout << "Knoten 2: " << center.searchNode(Point(2, 5))->value << "\n";
    cout << "Knoten 3: " << center.searchNode(Point(7, 6))->value << "\n";
    cout << "Nicht existierender Knoten: " << center.searchNode(Point(5, 5));
}