const usersData = [
  {id: 0, name: 'John Doe', lastLogin: '2018/01/01', role: 'Admin', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'},
  {id: 1, name: 'Samppa Nori', lastLogin: '2018/01/01', role: 'Trader', status: 'Active'},
  {id: 2, name: 'Estavan Lykos', lastLogin: '2018/02/01', role: 'Analyst', status: 'Inactive'},
  {id: 3, name: 'Chetan Mohamed', lastLogin: '2018/02/01', role: 'Operations', status: 'Inactive'},
  {id: 4, name: 'Derick Maximinus', lastLogin: '2018/03/01', role: 'Trader', status: 'Active'},
  {id: 5, name: 'Friderik Dávid', lastLogin: '2018/01/21', role: 'Analyst', status: 'Active'}
]

export default usersData
