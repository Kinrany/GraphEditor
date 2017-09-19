#include <iostream>
#include <vector>
#include <string>

using namespace std;

// skip the rest of the current line in stdin
void clearEOL() {
	getline(cin, string());
}

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

	clearEOL();
}

void writeMatrix() {
	for (int row = 0; row < n; ++row) {
		for (int column = 0; column < n; ++column) {
			cout << matrix[row][column] << ' ';
		}
		cout << endl;
	}
}

void readNodeColors() {
	nodeColors = vector<int>(n);
	for (int i = 0; i < n; ++i) {
		cin >> nodeColors[i];
	}

	clearEOL();
}

void writeNodeColors() {
	for (int i = 0; i < n; ++i) {
		cout << nodeColors[i] << ' ';
	}
	cout << endl;
}

void initializeEdgeColors() {
	edgeColors = vector<vector<int>>(n);
	for (int i = 0; i < n; ++i) {
		edgeColors[i] = vector<int>(n, 0);
	}
}

void readEdgeColors() {
	initializeEdgeColors();

	int node0, node1, color;
	while (true) {
		cin >> node0;
		if (node0 == -1) break;
		cin >> node1 >> color;

		edgeColors[node0][node1] = color;
	}

	clearEOL();
}

void writeEdgeColors() {
	for (int row = 0; row < n; ++row) {
		for (int column = 0; column < n; ++column) {
			cout << row << ' ' << column << ' ' << edgeColors[row][column] << endl;
		}
	}
	cout << -1 << endl;
}

void readText() {
	text = "";
	string str;
	while (getline(cin, str)) {
		text += str;
		text.push_back('\n');
	}
}

void writeText() {
	cout << text;
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

void write() {
	cout << n << endl;

	writeMatrix();

	if (!nodeColors.empty()) {
		cout << "Node colors:" << endl;
		writeNodeColors();
	}

	if (!edgeColors.empty()) {
		cout << "Edge colors:" << endl;
		writeEdgeColors();
	}

	if (!text.empty()) {
		cout << "Text:" << endl;
		writeText();
	}
}

void main() {
	read();
	write();
}
