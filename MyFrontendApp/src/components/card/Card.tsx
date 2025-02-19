import styles from './Card.module.scss'

const Card = () => {


    return(
        <div className={styles.container}>
            <div className={styles.img}>
            <div className={styles.content}>
                <div className={styles.contentDescription}>
                    <h4 className={styles.date}>Jan 1, 2020</h4>
                    <h3 className={styles.title}>Article Title</h3>
                </div>
                <div className={styles.contentLike}>
                    <h3 className={styles.like}>% S2</h3>

                </div>
            </div>
                <img src="https://images.unsplash.com/photo-1482877346909-048fb6477632?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=958&q=80" alt="" />
            </div>
        </div>
    )
}

export default Card
