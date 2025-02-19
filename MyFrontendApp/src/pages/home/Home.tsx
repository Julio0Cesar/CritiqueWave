import Card from "../../components/card/Card";
import styles from "./Home.module.scss"

const Home = () => {

  return (
    <div className="container">
      <div className={styles.cards}>
        <div className="card">
            <Card/>
            <Card/>
        </div>
      </div>
    </div>
  )
}

export default Home;
