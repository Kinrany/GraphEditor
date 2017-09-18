#include <iostream>
#include <vector>
#include <string>

using namespace std;

int n;
vector<vector<int>> matrix;
vector<int> nodeColors;
vector<vector<int>> edgeColors;
string text;

void readMatrix() {
	matrix = vector<vector<int>>(n);
	for (int row = 0; row < n; ++row) {
		matrix[row] = vector<int>(n);
		for (int column = 0; column < n; ++column) {
			cin >> matrix[row][column];
		}
	}
}

void readNodeColors() {
	nodeColors = vector<int>(n);
	for (int i = 0; i < n; ++i) {
		cin >> nodeColors[i];
	}
}

void readEdgeColors() {
	edgeColors = vector<vector<int>>(n);
	for (int i = 0; i < n; ++i) {
		edgeColors[i] = vector<int>(n, 0);
	}

	int node0, node1, color;
	while (true) {
		cin >> node0;
		if (node0 == -1) break;
		cin >> node1 >> color;

		edgeColors[node0][node1] = color;
	}
}

void readText() {
	text = "";
	string str;
	while (getline(cin, str)) {
		text += str;
		text.push_back('\n');
	}
}

void read() {
	cin >> n;

	readMatrix();

	string blockName;
	while (true) {
		getline(cin, blockName);
		if (blockName == "Node colors:") {
			readNodeColors();
		}
		else if (blockName == "Edge colors:") {
			readEdgeColors();
		}
		else if (blockName == "Text:") {
			readText();
			break;
		}
		else {
			break;
		}
	}
}

void main() {
	read();
}
