﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using ExtensionMethods;

namespace UILogicLibrary {
	public class Mouse {
		public Mouse() {
			_timer = new Timer();
			_timer.Elapsed += OnTimerElapsed;
		}

		public bool Left {
			get {
				return _left;
			}
			private set {
				Debug.Assert(_left != value);
				_left = value;
			}
		}
		public bool Right {
			get {
				return _right;
			}
			private set {
				Debug.Assert(_right != value);
				_right = value;
			}
		}

		public void LeftButtonDown(Point point) {
			Left = true;
			_startPoint = point;
			_timer.Start(_delay);
		}
		public void MouseMoved(Point point) {
			if (_timer.Enabled) {
				Debug.Assert(Left == true);
				_timer.Stop();
				LeftPressed(_startPoint);
			}
			Moved(point);
		}
		public void LeftButtonUp(Point point) {
			Left = false;
			if (_timer.Enabled) {
				LeftClick(point);
			}
			else {
				LeftDepressed(point);
			}
		}
		public void RightButtonDown(Point point) {
			Right = true;
		}
		public void RightButtonUp(Point point) {
			Right = false;
			RightClick(point);
		}

		public event MouseEventDelegate LeftClick;
		public event MouseEventDelegate RightClick;
		public event MouseEventDelegate LeftPressed;
		public event MouseEventDelegate LeftDepressed;
		//public event MouseEventDelegate RightPressed;
		//public event MouseEventDelegate RightDepressed;
		public event MouseEventDelegate Moved;

		Timer _timer;
		double _delay = 300;
		Point _startPoint;

		bool _left = false;
		bool _right = false;

		void OnTimerElapsed(object sender, ElapsedEventArgs e) {
			Debug.Assert(Left == true);
			LeftPressed(_startPoint);
		}
	}

	public delegate void MouseEventDelegate(Point point);	
}